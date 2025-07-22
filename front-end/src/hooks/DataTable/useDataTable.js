import { useEffect, useRef } from 'react';
import $ from 'jquery';
import 'datatables.net-bs4';
import 'datatables.net-buttons-bs4';
import 'datatables.net-responsive-bs4';
import 'datatables.net-buttons/js/buttons.html5';
import 'datatables.net-buttons/js/buttons.print';
import 'datatables.net-buttons/js/buttons.colVis';
import 'datatables.net-bs4/css/dataTables.bootstrap4.min.css';
import 'datatables.net-buttons-bs4/css/buttons.bootstrap4.min.css';
import 'datatables.net-responsive-bs4/css/responsive.bootstrap4.min.css';
import 'datatables.net-select-bs4';
import 'datatables.net-select-bs4/css/select.bootstrap4.min.css';
import JSZip from 'jszip';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';

const useDataTable = ({
    data,
    columnMap,
    columnHidden,
    showActions,
    detailLinkPrefix,
    updateLinkPrefix,
    enableMultiSelect = false,
    onSelectedChange,
    initialSelectedIds,
    navigate,
}) => {
    // Đối tượng dùng để xuất file Excel
    window.JSZip = JSZip;

    // Đối tượng dùng để xuất file PDF
    pdfMake.vfs = pdfFonts.vfs;

    const selectedIdsRef = useRef([]);

    useEffect(() => {
        if (enableMultiSelect && Array.isArray(initialSelectedIds)) {
            selectedIdsRef.current = initialSelectedIds;
            onSelectedChange?.(selectedIdsRef.current);
        }
    }, [initialSelectedIds, enableMultiSelect, onSelectedChange]);

    useEffect(() => {
        const normalizedData = data.map((item) => {
            const filled = {};
            Object.keys(columnMap).forEach((key) => {
                filled[key] = item[key] ?? '';
            });
            return { ...item, ...filled };
        });

        const baseColumns = [
            ...(enableMultiSelect
                ? [
                      {
                          title: '',
                          data: 'id',
                          orderable: false,
                          searchable: false,
                          className: 'text-center',
                          render: (data, type, row) =>
                              `<input type="checkbox" class="dt-checkbox" value="${row.id}" />`,
                      },
                  ]
                : []),
            {
                title: 'STT',
                data: null,
                orderable: true,
                searchable: false,
                className: 'text-center font-weight-bold',
                render: (data, type, row, meta) => meta.row + 1,
            },
            ...Object.keys(columnMap)
                .filter((key) => !columnHidden.includes(key))
                .map((key) => ({
                    data: key,
                    title: columnMap[key] || key,
                })),
        ];

        const columns = showActions
            ? [
                  ...baseColumns,
                  {
                      title: 'Ứng Dụng',
                      data: null,
                      orderable: false,
                      searchable: false,
                      className: 'text-center',
                      render: (data, type, row) => {
                          const updateBtn = updateLinkPrefix
                              ? `<button class="btn btn-success btn-sm mr-1 btn-update" data-id="${row.id}">
                              <i class="fas fa-edit"></i>
                         </button>`
                              : '';

                          const detailBtn = detailLinkPrefix
                              ? `<button class="btn btn-info btn-sm btn-detail" data-id="${row.id}">
                              <i class="fas fa-info-circle"></i>
                         </button>`
                              : '';

                          return `${updateBtn}${detailBtn}`;
                      },
                  },
              ]
            : baseColumns;

        const config = {
            destroy: true,
            responsive: true,
            lengthChange: false,
            autoWidth: false,
            data: normalizedData,
            columns,
            buttons: [
                {
                    extend: 'copy',
                    exportOptions: { columns: ':not(:last-child)' },
                },
                {
                    extend: 'csv',
                    exportOptions: { columns: ':not(:last-child)' },
                },
                {
                    extend: 'excel',
                    exportOptions: { columns: ':not(:last-child)' },
                },
                {
                    extend: 'pdf',
                    exportOptions: { columns: ':not(:last-child)' },
                },
                {
                    extend: 'print',
                    exportOptions: { columns: ':not(:last-child)' },
                },
                'colvis',
            ],
            language: {
                emptyTable: 'Không có dữ liệu',
                zeroRecords: 'Không tìm thấy kết quả phù hợp',
                search: 'Tìm kiếm',
                paginate: {
                    previous: 'Trước',
                    next: 'Sau',
                },
            },
        };

        const table = $('#tabledata').DataTable(config);

        if (enableMultiSelect && onSelectedChange) {
            // Xử lý sự kiện change cho checkbox
            $('#tabledata').off('change', '.dt-checkbox');
            $('#tabledata').on('change', '.dt-checkbox', (e) => {
                const checkbox = e.target;
                const id = Number(checkbox.value); // Chuyển value thành số (long)
                const isChecked = checkbox.checked;

                let selectedIds = [...selectedIdsRef.current];
                if (isChecked) {
                    if (!selectedIds.includes(id)) {
                        selectedIds.push(id);
                    }
                } else {
                    selectedIds = selectedIds.filter((selectedId) => selectedId !== id);
                }

                selectedIdsRef.current = selectedIds;
                onSelectedChange(selectedIds);
                console.log('Selected IDs:', selectedIds); // Debug
            });

            // Đặt lại trạng thái checkbox khi DataTable redraw
            table.on('draw', () => {
                $('#tabledata .dt-checkbox').each((index, checkbox) => {
                    const id = Number(checkbox.value); // Chuyển value thành số
                    checkbox.checked = selectedIdsRef.current.includes(id);
                });
            });
        }

        table.buttons().container().appendTo('#tabledata_wrapper .dt-layout-start:eq(0)');

        $('#tabledata')
            .off('click', '.btn-detail, .btn-update')
            .on('click', '.btn-detail, .btn-update', function (e) {
                e.preventDefault();
                const id = $(this).data('id');

                if ($(this).hasClass('btn-detail')) {
                    navigate(`${detailLinkPrefix}/${id}`);
                } else if ($(this).hasClass('btn-update')) {
                    navigate(`${updateLinkPrefix}/${id}`);
                }
            });

        return () => {
            table.destroy();
        };
    }, [
        data,
        columnMap,
        columnHidden,
        showActions,
        detailLinkPrefix,
        updateLinkPrefix,
        navigate,
        enableMultiSelect,
        onSelectedChange,
    ]);
};

export default useDataTable;
