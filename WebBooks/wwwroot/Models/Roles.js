function Delete(id) {
    Swal.fire({
        title: "هل انت متـأكد ؟",
        text: "لن تتمكن من التراجع عن هذا !",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Accounts/DeleteRole?Id=${id}`
            Swal.fire({
                title: "تم الحذف",
                text: "تم حذف مجموعة المستخدم",
                icon: "success"
            });
        }
    });
}

function Edit()