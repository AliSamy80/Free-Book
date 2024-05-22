//let table = new DataTable('#tableRole');
//let table = new DataTable('#tableRole');
//$(document).ready(function () {
//    $('#tableRole').DataTable({
//        "autoWidth": false,
//        "responsive": true
//    });
//});

$(document).ready(function () {
    if (!$.fn.DataTable.isDataTable('#tableRole')) {
        $('#tableRole').DataTable({
            "autoWidth": false,
            "responsive": true
        });
    }
});

function Delete(id) {
    console.log("Delete function called with ID:", id); // Check if the function is being triggered and if the ID is correct
    Swal.fire({
        title: "هل انت متـــأكد ؟",
        text: "لن تتمكن من الرجوع في هذا !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "yes, delete it !",
        cancelButtonText:  "cancel !"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Accounts/DeleteUser?Id=${id}`;
            Swal.fire(
                "تم الحذف",
                "تم حذف مجموعة المستخـــدم !",
                "نجـــاح"
            )
        }
    })
}

Edit = (id, name, image, email, role, active) => {
    document.getElementById("title").innerHTML = "تعديل مجموعة المستخدم";
    document.getElementById("btnSave").value = "تعديل";

    document.getElementById("userId").value = id;
    document.getElementById("userName").value = name;
    //document.getElementById("userImage").value = image;
    document.getElementById("userEmail").value = email;
    console.log(`Email => ${email}`)
    document.getElementById("ddluserRole").value = role;
    //var active = document.getElementById("userActive") ;

    //if (active == "True") {

    //    active.checked = true;

    //} else {

    //    active.checked = false;
    //}

    var isActive = (active == "true"); // Convert to boolean


    document.getElementById("userActive").checked = isActive; // Set checkbox state based on boolean

    $('#grPassword').hide();
    $('#grcomPassword').hide();

    document.getElementById("userPassword").value = "$$$$$$";
    document.getElementById("userComparePassword").value = "$$$$$$";

    // Show the image
    document.getElementById("image").hidden = false;
    document.getElementById("image").value = image;
    document.getElementById("image").src = "/Images/Users/" + image;
}


Rest = () => {
    document.getElementById("title").innerHTML = "اضف مجموعة جديــدة";
    document.getElementById("btnSave").value = "حفظ";
    document.getElementById("userId").value = '';
    document.getElementById("userName").value = '';
    document.getElementById("userEmail").value = '';
    //document.getElementById("userImage").value = image;
    document.getElementById("ddluserRole").value = '';
    document.getElementById("userActive").checked = false;

    // Show password inputs
    $('#grPassword').show();
    $('#grcomPassword').show();

    // Clear password inputs
    document.getElementById("userPassword").value = "";
    document.getElementById("userComparePassword").value = "";
    document.getElementById("image").hidden = true;
}

function ChangePassword(id) {
    document.getElementById('userPassId').value = id;
    console.log(`Email => ${id}`)

}
