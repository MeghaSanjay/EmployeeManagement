
$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
   
});

function bindEvents() {
    $(".employeeDetails").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");

        $.ajax({
            url: 'https://localhost:6001/api/internal/employees/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);
                showEmployeeDetailCard();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        var result =confirm("Are you sure want to delete");
        if (result) {
            alert("Sucessfully deleted");
            $.ajax({
                url: 'https://localhost:6001/api/internal/employees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    location.reload();
                },

                error: function (error) {
                    console.log(error);
                }
            });
        }
        else {
            alert("Not able to delete");
        }
    });
    $("#btnAddSave").on("click", function (event) {
        
        var employeeName = $("#insertName").val();
        var employeeDepartment = $("#insertDepartment").val();
        var employeeAge = $("#insertAge").val();
       var employeeAddress = $("#insertAddress").val();

  
        let employee = {
            name: employeeName,
            department: employeeDepartment,
            age: parseInt( employeeAge),
            address: employeeAddress
        };
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: 'https://localhost:6001/api/internal/employees',
            type: 'POST',
            data: JSON.stringify(employee),
            dataType: 'json',
            success: function (result) {

                location.reload();    
            },
            error: function (error) {
                console.log(error);
            }

        })
        });

       


    $(".employeeEdit").on("click", function (event) {
       
        var employeeId = event.currentTarget.getAttribute("data-id");
        $.ajax({
            url: 'https://localhost:6001/api/internal/employees/' + employeeId,
            type: 'PUT',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                
            },
            error: function (error) {
                console.log(error);
            }

        })
     
    });
}



function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}