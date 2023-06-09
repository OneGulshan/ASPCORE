var dtable;
$(document).ready(function () {

    var url = window.location.search; // here our window location searched for nowing status of order by including
    if (url.includes("pending")) {
        OrderTable("pending");
    }
    else {
        if (url.includes("approved")) {
            OrderTable("approved");
        }
        else {
            if (url.includes("shipped")) {
                OrderTable("shipped");
            }
            else {
                if (url.includes("underprocess")) {
                    OrderTable("underprocess"); 
                }
                else {
                    OrderTable("all");
                }
            }
        }
    }
});
function OrderTable(status) {
    dtable = $('#myTable').DataTable({

        "ajax": { "url": "/Admin/Order/AllOrders?status=" + status },
        "columns": [
            { "data": "name" },/*Json file ko read karne ki bajae Model ko read karen*/ /*we required write text here in cammel notation, we write text here in upper camel notation 1st latter in small and second letter in Big json result aise hi kaam karta hai*/
            { "data": "phone" },
            { "data": "orderStatus" },
            { "data": "orderTotal" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <a href="/Admin/order/orderDetails?id=${data}"><i class="bi bi-pencil-square"></i></a>
`
                }
            }
        ]
    });
}

//Cammel Case Example
//upper camel case, a.k.a. PascalCase: ThisIsAnExample.thisIsNotAnExample.
//lower camel case: thisIsAnExample.ThisIsNotAnExample.