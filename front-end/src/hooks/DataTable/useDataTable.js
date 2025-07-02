import { useEffect } from 'react';
import $ from 'jquery';
import 'datatables.net-bs4';
import 'datatables.net-buttons-bs4';
import 'datatables.net-responsive-bs4';
import 'datatables.net-buttons/js/buttons.html5';
import 'datatables.net-buttons/js/buttons.print';
import 'datatables.net-buttons/js/buttons.colVis';

const useDataTable = ({ data, columnMap, showActions, detailLinkPrefix, updateLinkPrefix, navigate }) => {
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
              .filter((key) => key !== 'id')
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

    const table = $('#tabledata').DataTable({
      destroy: true,
      responsive: true,
      lengthChange: false,
      autoWidth: false,
      data,
      columns,
      buttons: ['copy', 'csv', 'excel', 'pdf', 'print', 'colvis'],
      language: {
        emptyTable: 'Không có dữ liệu',
        zeroRecords: 'Không tìm thấy kết quả phù hợp',
        search: 'Tìm kiếm:',
        paginate: {
          previous: 'Trước',
          next: 'Sau',
        },
      },
    });

    table.buttons().container().appendTo('#tabledata_wrapper .dt-layout-start:eq(0)');

    $('#tabledata').on('click', '.btn-detail, .btn-update', function (e) {
      e.preventDefault();
      const id = $(this).data('id');

      if ($(this).hasClass('btn-detail')) {
        navigate(`${detailLinkPrefix}`);
      } else if ($(this).hasClass('btn-update')) {
        navigate(`${updateLinkPrefix}`);
      }
    });

    return () => {
      table.destroy();
    };
  }, [data, columnMap, showActions, detailLinkPrefix, updateLinkPrefix, navigate]);
};

export default useDataTable;
