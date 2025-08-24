$(document).ready(function () {
    $('.js-delete').on('click', function (e) {
        e.preventDefault();

        var btn = $(this);

        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-secondary mx-3",
                cancelButton: "btn btn-primary"
            },
            buttonsStyling: false
        });

        swalWithBootstrapButtons.fire({
            title: "Are you sure?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: `/Courses/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        swalWithBootstrapButtons.fire({
                            title: "Deleted!",
                            text: "Course has been deleted.",
                            icon: "success"
                        });
                        btn.closest('tr').fadeOut();
                    },
                    error: function () {
                        swalWithBootstrapButtons.fire({
                            title: "Error",
                            text: "Something went wrong.",
                            icon: "error"
                        });
                    }
                });

            } else if (result.dismiss === Swal.DismissReason.cancel) {
                swalWithBootstrapButtons.fire({
                    title: "Cancelled",
                    text: "Your course is safe :)",
                    icon: "error"
                });
            }
        });
    });
});