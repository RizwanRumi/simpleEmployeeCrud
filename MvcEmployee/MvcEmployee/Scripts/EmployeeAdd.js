function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imageView').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$("#imageFile").change(function () {
    readURL(this);
});


$("#btnCreate").click(function (e) {
   e.preventDefault();
   $('#btnCreate').prop('disabled', true);
            
   var firstName = $("#firstName").val();
   var lastName = $("#lastName").val();
   var email = $("#email").val();
   var form = document.getElementById("gender");
   var gender = form.elements["gender"].value;

   var data = new FormData();
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
       url: 'Create',
       contentType: false,
       processData: false,
       async: false,
       data: data,
       success: function (result) {
           if (result.valid == 1) {
               alert(result.message);
               $('#btnCreate').prop('disabled', false);
               location.reload(true);
               return false;
           }
           else if (result.valid == 2) {
               alert(result.message);
               $('#btnCreate').prop('disabled', false);
               return false;
           }
           //} else {
           //    alert(result.message);
           //    $('#btnCreate').prop('disabled', false);
           //    return false;
           //}
       },
       error: function () {
           alert("Critical Error!. Failed to call the server.");
           return false;
       }
   });

});


        
      
