//let table = new DataTable('#tableRole');
//let table = new DataTable('#tableRole');
$(document).ready(function () {
    $('#tableRole').DataTable();
});
//function Delete(id) {
//    Swal.fire({
//        title: lbTitleMsgDelete,
//        text: lbTextMsgDelete,
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: lbconfirmButtonText,
//        cancelButtonText: lbcancelButtonText
//    }).then((result) => {
//        if (result.isConfirmed) {
//            window.location.href = `/Admin/Accounts/DeleteRole?Id=${id}`;
//            Swal.fire(
//                lbTitleDeletedOk,
//                lbMsgDeletedOkRole,
//                lbSuccess
//            )
//        }
//    })
//}
debugger
function Delete(id) {
    console.log(`Attempting to delete role with id: ${id}`);
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
            console.log(`Confirmed deletion for role id: ${id}`);
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
    document.getElementById("title").innerHTML = lbTitleEdit;
    document.getElementById("btnSave").value = LbBtnEdit;
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}

Rest = () => {
    document.getElementById("title").innerHTML = lbAddNewRole;
    document.getElementById("btnSave").value = LbBtnSave;
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
}
