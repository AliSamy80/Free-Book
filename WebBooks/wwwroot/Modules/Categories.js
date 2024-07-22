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
        $('#tableCategory').DataTable({ // 
            "autoWidth": false,
            "responsive": true
        });
    }
});

$(document).ready(function () {
    if (!$.fn.DataTable.isDataTable('#tableRole')) {
        $('#tableLogCategory').DataTable({ // tableLogCategory
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
        cancelButtonText: "cancel !"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Categories/Delete?Id=${id}`;
            Swal.fire(
                "تم الحذف",
                "تم حذف مجموعة الفئات !",
                "نجـــاح"
            )
        }
    })
}


function DeleteLog(id) {
    console.log("Delete function called with ID:", id); // Check if the function is being triggered and if the ID is correct
    Swal.fire({
        title: "هل انت متـــأكد ؟",
        text: "لن تتمكن من الرجوع في هذا !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "yes, delete it !",
        cancelButtonText: "cancel !"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Categories/DeleteLog?Id=${id}`;
            Swal.fire(
                "تم الحذف",
                "تم حذف مجموعة الفئات !",
                "نجـــاح"
            )
        }
    })
}




Edit = (id, name, description) => {
    //document.getElementById("title").innerHTML = lbTitleEdit;
    document.getElementById("title").innerHTML = "تعديل الفئة";
    document.getElementById("btnSave").value = "تعديل";
    document.getElementById("categoryId").value = id;
    document.getElementById("categoryName").value = name;
    document.getElementById("categoryDiscription").value = description;
}


Rest = () => {
    document.getElementById("title").innerHTML = "اضف فئة جديــدة";
    //document.getElementById("title").innerHTML = lbAddNewCategory;
    //document.getElementById("btnSave").value = LbBtnSave;
    document.getElementById("btnSave").value = 'حفظ';
    document.getElementById("categoryId").value = '';
    document.getElementById("categoryName").value = '';
    document.getElementById("categoryDiscription").value = '';
}



document.getElementById("defaultOpen").click();

function openCity(evt, cityName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}