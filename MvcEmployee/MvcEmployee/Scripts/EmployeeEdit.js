function readURL(input) {
    
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function(e) {
            $("#imageView").attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$("#imageFile").change(function() {
    readURL(this);
});


$("#btnEdit").click(function (e) {
    e.preventDefault();
    $("#btnEdit").prop('disabled', true);

    var employeeId = $("#employeeId").val();
    var firstName = $("#firstName").val();
    var lastName = $("#lastName").val();
    var email = $("#email").val();
    var form = document.getElementById("gender");
    var gender = form.elements["gender"].value;
    
   
    var data = new FormData();
    data.append("employeeId", employeeId);
    data.append("firstName", firstName);
    data.append("lastName", lastName);
    data.append("email", email);
    data.append("gender", gender);

    var files = $("#imageFile").get(0).files;

    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        data.append("UploadedImage", files[0]);
    }

    $.ajax({
        type: 'POST',
        url: 'Edit',
        contentType: false,
        processData: false,
        async: false,
        data: data,
        success: function(result) {
            if (result.valid == 1) {
                alert(result.message);
                $('#btnEdit').prop('disabled', false);
                location.reload(true);
                return false;
            } else if (result.valid == 2) {
                alert(result.message);
                $('#btnEdit').prop('disabled', false);
                return false;
            }
        },
        error: function () {
            alert("Critical Error!. Failed to call the server.");
            return false;
        }
    });
    
});