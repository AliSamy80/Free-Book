//let table = new DataTable('#tableRole');
//let table = new DataTable('#tableRole');
$(document).ready(function () {
    $('#tableRole').DataTable();
});
function Delete(id) {
    Swal.fire({
        title: lbTitleMsgDelete,
        text: lbTextMsgDelete,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Accounts/DeleteRole?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkRole,
                lbSuccess
            )
        }
    })
}

Edit = (id, name) => {
    document.getElementById("title").innerHTML = "تعديل مججموعة المستخدم";
    document.getElementById("btnSave").value = "تعديل";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}

Rest = () => {
    document.getElementById("title").innerHTML = "اضف مجموعة جديــدة";
    document.getElementById("btnSave").value = "حفظ";
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
}
