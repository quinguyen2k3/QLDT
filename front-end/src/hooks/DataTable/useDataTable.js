import { useEffect } from 'react';
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
    navigate,
}) => {
    // Đối tượng dùng để xuất file Excel
    window.JSZip = JSZip;

    // Đối tượng dùng để xuất file PDF
    pdfMake.vfs = pdfFonts.vfs;

    useEffect(() => {
        const baseColumns =
            data.length > 0
                ? [
                      {
                          title: 'STT',
                          data: null,
                          orderable: true,
                          searchable: false,
                          className: 'text-center font-weight-bold',
                          render: (data, type, row, meta) => meta.row + 1,
                      },
                      ...Object.keys(data[0])
                          .filter((key) => key !== 'id' && !columnHidden.includes(key))
                          .map((key) => ({
                              data: key,
                              title: columnMap[key] || key,
                          })),
                  ]
                : [];

        const columns = showActions
            ? [
                  ...baseColumns,
                  {
                      title: 'Ứng Dụng',
                      data: null,
                      orderable: false,
                      searchable: false,
                      className: 'text-center',
                      render: (data, type, row) => `
                        <button class="btn btn-success btn-sm mr-1 btn-update" data-id="${row.id}">
                            <i class="fas fa-edit"></i>
                        </button>
                        <button class="btn btn-info btn-sm btn-detail" data-id="${row.id}">
                            <i class="fas fa-info-circle"></i>
                        </button>
                      `,
                  },
              ]
            : baseColumns;

        const config = {
            destroy: true,
            responsive: true,
            lengthChange: false,
            autoWidth: false,
            data,
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

        //Bật tắt multi select
        if (enableMultiSelect) {
            config.select = { style: 'multi' };
        }

        const table = $('#tabledata').DataTable(config);

        table.buttons().container().appendTo('#tabledata_wrapper .dt-layout-start:eq(0)');

        $('#tabledata').on('click', '.btn-detail, .btn-update', function (e) {
            e.preventDefault();
            const id = $(this).data('id');

            if ($(this).hasClass('btn-detail')) {
                navigate(`${detailLinkPrefix}`);
            } else if ($(this).hasClass('btn-update')) {
                navigate(`${updateLinkPrefix}/${id}`);
            }
        });

        return () => {
            table.destroy();
        };
    }, [data, columnMap, columnHidden, showActions, detailLinkPrefix, updateLinkPrefix, navigate, enableMultiSelect]);
};

export default useDataTable;
