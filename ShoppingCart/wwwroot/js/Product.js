// We can write our js file name JavaScript as keyword(noproblem), if we change file name after create then js file does,nt render req to re-create
// ajax data ko partialy load kar ke show kar deta hai jisse hamara page bina load hue hamein hamara data mil jata hai, kunki ajax backgroung process par zyada kaam karta hai, so
var dtable;
$(document).ready(function () {//jo bhi data table hai hamari datatable use apni shape me badal dega using this simple code
    dtable = $('#myTable').DataTable({//DataTable me hamein data formates ka use karna hoga
        "ajax": {//used ajax ek prakar ka array hoga jisne ham apna url use karenge
            "url": "/Admin/Product/AllProducts"
        },
        "columns": [//in our columns array a dictionary(key(data)+value(name,position etc)) type object array
            { "data": "name" },//name ke ander name jaega json result me kunki ye hota hai case sensitive //Product model data rendered by this data variable, 1st key is data with its value by column name, column name is defined here according our json results
            { "data": "description" },
            { "data": "price" },
            { "data": "category.name" },//jo json result hamare pass aa rha hai usi json result ki prop ka use karo isme
            {
                "data": "id",
                "render": function (data) {//Html ko render karane ke liye hamein render nam ka func use karna hota hai, render use hota hai jab ham kisi bhi data ko asetice html view dena chate hai, data me hamari id hai to ab hamein data use karne hai
                    return `
<a href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                    <a onClick=RemoveProduct("/Admin/Product/Delete/${data}")><i class="bi bi-trash"></i></a>

`
                }
            }
        ]
    });
});

function RemoveProduct(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();//for reloading ajax
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
