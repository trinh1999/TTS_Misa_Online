$(document).ready(function() {
    formModel = 0;
    employeeId = null;
	//load dl
    loadData();
    // thực hiện load display
    // 1. Gọi API lấy dl
    $.ajax(
        {
        url:"http://cukcuk.manhnv.net/v1/Employees", //Địa chỉ API
        method:"GET", //Phương thức GET lấy dl, POST- Thêm mớ

        }
    ).done(function(res){
        // 2. Xử lý dl
        // console.log(res);
        let employee = res;

        // 2.1.Duyệt từng đối tượng có trong mảng  dl và thực hiện build
        res.forEach(employee=>{
            //lấy từng dl
            // console.log(employee);
            const employeeCode = employee.EmployeeCode;
            const fullName = employee.FullName;
            const genderName = employee.GenderName;
            const dateOfBirth = employee.DateOfBirth;
            const dateString = formatDate(dateOfBirth);
            const phoneNumber = employee.PhoneNumber;
            const email = employee.Email;
            const positionName = employee.PositionName;
            const departmentName = employee.DepartmentName;
            const salary = employee.Salary;
            const salaryFormat = formatSalary(salary);
            const workStatus = employee.WorkStatus;
            // const employeeId = employee.EmployeeId;
           
            const trHTML = `<tr employeeId = ${employee.EmployeeId}>
                                <td>${employeeCode}</td>
                                <td>${fullName}</td>
                                <td>${genderName}</td>
                                <td>${dateString}</td>
                                <td>${phoneNumber}</td>
                                <td>${email}</td>
                                <td>${positionName}</td>
                                <td>${departmentName}</td>
                                <td>${salaryFormat}</td>
                                <td>${workStatus}</td>
                            </tr>`;
        //append trHTML vào tbody
        $('tbody').append(trHTML);
        //đẩy lên đầu
        // $('tbody').prepend(trHTML);
        // $(trHTML).attr('employeeId',employee.EmployeeId);
                            

        });
        // 3. Hiển thị dl lên table
    }).fail(function(res){
        //Đưa ra thông báo lỗi
        //thông thường
        // 400-BadRequest-Lỗi dl đầu vào
        // 404-Địa chỉ url ko hợp Lệ
        // 500-lỗi từ phía backend-Service
    })

    // Khởi tạo các sự kiện
// 1. Thêm mới: Hiển thị form thêm ms 
// Author: DTTrinh(20/7/2021)
$('#btnAdd').click(btnAddOnClick);

// 2. Validate dl (id input vào)
$('input[required]').blur(function() {
    //Kiểm tra thông tin có nhập ko
    let value=$(this).val();
    if (value==''){
        //Thay đổi border của input thành màu đỏ
        $(this).css('border', '1px solid red')
        $(this).attr('title', 'Thông tin này bắt buộc nhập')
    } else {
        $(this).css('border', '1px solid #bbbbbb')
        $(this).removeAttr('title');
    }
})

//3.hủyform
$('.button.btn1').click(function() {
    $('#dialogEmployee').hide();
    $('.list').css('opacity', '1')
})
$('#Cancel').click(function() {
    $('#dialogEmployee').hide();
    $('.list').css('opacity', '1')
})


//4.Save lưu
$('.button.btn2').click(btnSaveOnClick);


// 5. sửa dl
$('table#listEmployees').on('dblclick','tbody tr', function(){
    //hiển thị form chi tiết
    formModel = 0;
    $('#dialogEmployee').show();
    $('.list').css('opacity', '0.5')
    $('.list').css('position', 'fixed')
    
    // load dl cho combobox
    // 1. vị trí
    $.ajax({
        url:'http://cukcuk.manhnv.net/v1/Positions',
        method:'GET',
    }).done(res => {
        console.log(res);
        $('.options-container.box22').empty();
        res.forEach(position=>{
            const posName=position['PositionName'];
            const positionId=position['PositionId'];
            let optionHTML=`<div class="option box22">
                                <input type="radio" class="radio" name="category"/>
                                <label class="text-dropdown" Value="${positionId}" for="">${posName}</label>
                            </div>`;
            $('.options-container.box22').append(optionHTML);
            const optionsList = document.querySelectorAll(".option");
            optionsList.forEach(n => {
            n.addEventListener("click", () => {
            let text = n.querySelector("label").innerHTML;
            n.closest('.options-container.box22').classList.remove("active");
            n.closest('.options-container.box22').nextElementSibling.innerHTML= text;
            });
        });
        });
    })

    // // 2. phòng ban
    $.ajax({
        url:'http://cukcuk.manhnv.net/api/Department',
        method:'GET',
    }).done(res => {
        console.log(res);
        $('.options-container.box33').empty();
        res.forEach(position=>{
            const departmentName=position['DepartmentName'];
            const departmentId=position['DepartmentId'];
            let optionHTML=`<div class="option box33">
                                <input type="radio" class="radio" name="category"/>
                                <label class="text-dropdown" Value="${departmentId}" for="">${departmentName}</label>
                            </div>`;
            $('.options-container.box33').append(optionHTML);
            const optionsList = document.querySelectorAll(".option");
            optionsList.forEach(n => {
            n.addEventListener("click", () => {
            const text = n.querySelector("label").innerHTML;
            n.closest('.options-container.box33').classList.remove("active");
            n.closest('.options-container.box33').nextElementSibling.innerHTML= text;
            });
        });
        });
    })

    // lấy id input
    employeeId = $(this).attr("employeeId");
    // alert(employeeId);
    $.ajax({
        url:"http://cukcuk.manhnv.net/v1/Employees/" + employeeId,
        method:"GET",
        dataType:"json",
    }).done(function(res){
        alert('Đã lấy đc id');
        $('#txtEmployeeCode').val(res.EmployeeCode);
        $('#txtFullName').val(res.FullName);
        $('#dateDateOfBirth').val(res.DateOfBirth.slice(0,10));
        $('#txtGender').attr("employeeId", `${res.Gender}`);
        $('#txtIdentityNumber').val(res.IdentityNumber);
        $('#txtIdentityDate').val(res.IdentityDate.slice(0,10));
        $('#txtIdentityPlace').val(res.IdentityPlace);
        $('#txtEmail').val(res.Email);
        $('#txtPhoneNumber').val(res.PhoneNumber);
        $('#txtPositionName').attr("Value", `${res.PositionId}`);
        console.log(res.PositionName);
        $('#txtPositionName').text(`${res.PositionName}`);
        $('#txtDepartmentName').attr("Value", `${res.DepartmentId}`);
        $('#txtDepartmentName').text(`${res.DepartmentName}`);
        $('#txtPersonalTaxCode').val(res.PersonalTaxCode);
        $('#txtSalary').val(formatSalary(res.Salary));
        $('#txtJoinDate').val(res.JoinDate.slice(0,10));
        $('#txtWorkStatus').attr("employeeId", `${res.WorkStatus}`);
      
    }).fail(function(res){

    });
})

//6.Xóa nhân viên
$('table#listEmployees').on('mousedown', 'tr', function(event) {
    if (event.which == 3) {
        console.log('Click chuột phải để xóa');
        if (confirm("Bạn muốn xóa nhân viên này?")) {
            employeeId = $(this).attr("employeeId");
            deleteEmployee(employeeId);

        }
    }
});

// 7. Tìm kiếm nhân viên
$('#search-employee').on('keyup', function(event) {
    event.preventDefault();
    /* Act on the event */
    var key = $(this).val().toLowerCase();
    $('tbody tr').filter(function() {
       $(this).toggle($(this).text().toLowerCase().indexOf(key)>-1);
    });
 });
    
})


// Định dạng lại ngày sinh
// @param {any} date
// CreatedBy: ĐT.Trinh
function formatDate(dateInput) {
    let dateString='';
    if (dateInput!=null || dateInput!=undefined){
    let newDate = new Date(dateInput);
    var date = newDate.getDate();
    var month = newDate.getMonth();
    var year = newDate.getFullYear();
    month = (month < 10) ? "0" + month : month;
    date = (date < 10) ? "0" + date : date;
    dateString = `${date}/${month}/${year}`;
    // console.log(dateString);
    }
    return dateString;
}
// Định dạng lại lương
// @param {number} money
// CreatedBy: ĐT.Trinh
function formatSalary(salaryInput){
    if (salaryInput||salaryInput==0) {
        return salaryInput.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
    }
    return null;
}

function btnAddOnClick() {
    formModel = 1;
    $('#dialogEmployee').show();
    $('.list').css('position', 'fixed')
    $('.list').css('opacity', '0.3')
    //reset form to
    $('#dialogEmployee input').val(null);
    //lấy mã nv ms và thực hiện binding vào input 
    $.ajax({
        url:'http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode',
        method:'GET',
    }).done(res=>{
        $('#txtEmployeeCode').val(res);
        $('#txtEmployeeCode').focus();
    })

    // load dl cho combobox
    // 1. vị trí
    $.ajax({
        url:'http://cukcuk.manhnv.net/v1/Positions',
        method:'GET',
    }).done(res => {
        console.log(res);
        $('.options-container.box22').empty();
        res.forEach(position=>{
            const posName=position['PositionName'];
            const positionId=position['PositionId'];
            let optionHTML=`<div class="option box22">
                                <input type="radio" class="radio" name="category"/>
                                <label class="text-dropdown" Value="${positionId}" for="">${posName}</label>
                            </div>`;
            $('.options-container.box22').append(optionHTML);
            const optionsList = document.querySelectorAll(".option");
            optionsList.forEach(n => {
            n.addEventListener("click", () => {
            let text = n.querySelector("label").innerHTML;
            n.closest('.options-container.box22').classList.remove("active");
            n.closest('.options-container.box22').nextElementSibling.innerHTML= text;
            });
        });
        });
    })

    // // 2. phòng ban
    $.ajax({
        url:'http://cukcuk.manhnv.net/api/Department',
        method:'GET',
    }).done(res => {
        console.log(res);
        $('.options-container.box33').empty();
        res.forEach(position=>{
            const departmentName=position['DepartmentName'];
            const departmentId=position['DepartmentId'];
            let optionHTML=`<div class="option box33">
                                <input type="radio" class="radio" name="category"/>
                                <label class="text-dropdown" Value="${departmentId}" for="">${departmentName}</label>
                            </div>`;
            $('.options-container.box33').append(optionHTML);
            const optionsList = document.querySelectorAll(".option");
            optionsList.forEach(n => {
            n.addEventListener("click", () => {
            let text = n.querySelector("label").innerHTML;
            n.closest('.options-container.box33').classList.remove("active");
            n.closest('.options-container.box33').nextElementSibling.innerHTML= text;
            });
        });
        });
    })

}

function btnSaveOnClick(){
    let employee={
        "createdDate": "2021-07-21T03:21:27.526Z",
        "createdBy": "string",
        "modifiedDate": "2021-07-21T03:21:27.526Z",
        "modifiedBy": "string",
        "employeeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "employeeCode": "string",
        "firstName": "string",
        "lastName": "string",
        "fullName": "string",
        "gender": 0,
        "dateOfBirth": "2021-07-21T03:21:27.526Z",
        "phoneNumber": "string",
        "email": "string",
        "address": "string",
        "identityNumber": "string",
        "identityDate": "2021-07-21T03:21:27.526Z",
        "identityPlace": "string",
        "joinDate": "2021-07-21T03:21:27.526Z",
        "martialStatus": 0,
        "educationalBackground": 0,
        "qualificationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "departmentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "positionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "workStatus": 0,
        "personalTaxCode": "string",
        "salary": 0,
        "positionCode": "string",
        "positionName": "string",
        "departmentCode": "string",
        "departmentName": "string",
        "qualificationName": "string"
    };
    //TODO: validate toàn bộ dl 

    // thu thập dl-> build thành object nhân viên
    employee.employeeCode = $('#txtEmployeeCode').val();
    employee.fullName = $('#txtFullName').val();
    employee.dateOfBirth = $('#txtDateOfBirth').val();
    employee.gender = $('#txtGender').attr("Value");
    employee.identityNumber = $('#txtIdentityNumber').val();
    employee.identityDate = $('#txtIdentityDate').val();
    employee.identityPlace = $('#txtIdentityPlace').val();
    employee.email = $('#txtEmail').val();
    employee.phoneNumber = $('#txtPhoneNumber').val();
    employee.positionId = $('#txtPositionName').attr("Value");
    employee.departmentId = $('#txtDepartmentName').attr("Value");
    employee.personalTaxCode = $('#txtPersonalTaxCode').val();
    employee.salary = $('#txtSalary').val();
    employee.joinDate = $('#txtJoinDate').val();
    employee.workStatus = $('#txtWorkStatus').attr("Value");
    //TH dl ko hợp Lệ
    // employee.employeeCode = null;
    let method = 'POST';
    let url = 'http://cukcuk.manhnv.net/v1/Employees';
    if (formModel == 0){
        method = "PUT";
        url = "http://cukcuk.manhnv.net/v1/Employees/" + employeeId;
    }
    // gọi đến API thực hiện dl
    $.ajax({
        url: url,
        method:method,
        data: JSON.stringify(employee),
        dataType:"json",
        contentType:"application/json"
    }).done(res =>{
        alert('Thành công');
        $('.list').css('opacity', '1')
        //load lại dl
        loadData();
        $('#dialogEmployee').hide();
    }).fail(res =>{
        switch(res.status){
            case 500:
                alert('Có lỗi xảy ra');
                break;
            case 400:
                alert('Dữ liệu không hợp lệ');
            default:
                break;
        }
    })

}

//load dl danh sách
function loadData(){
    try {
          // cler dl cũ hiện trong bảng
    $('tbody').empty();
    $.ajax(
        {
        url:"http://cukcuk.manhnv.net/v1/Employees", //Địa chỉ API
        method:"GET", //Phương thức GET lấy dl, POST- Thêm mớ
        }
    ).done(function(res){
        // 2. Xử lý dl
        // console.log(res);
        let employee = res;

        // 2.1.Duyệt từng đối tượng có trong mảng  dl và thực hiện build
        res.forEach(employee=>{
            //lấy từng dl
            // console.log(employee);
            const employeeCode = employee.EmployeeCode;
            const fullName = employee.FullName;
            const genderName = employee.GenderName;
            const dateOfBirth = employee.DateOfBirth;
            const dateString = formatDate(dateOfBirth);
            const phoneNumber = employee.PhoneNumber;
            const email = employee.Email;
            const positionName = employee.PositionName;
            const departmentName = employee.DepartmentName;
            const salary = employee.Salary;
            const salaryFormat = formatSalary(salary);
            const workStatus = employee.WorkStatus;
            // const employeeId = employee.EmployeeId;
            // console.log(dateString);
            const trHTML = `<tr employeeId = "${employee.EmployeeId}">
                                <td>${employeeCode}</td>
                                <td>${fullName}</td>
                                <td>${genderName}</td>
                                <td>${dateString}</td>
                                <td>${phoneNumber}</td>
                                <td>${email}</td>
                                <td>${positionName}</td>
                                <td>${departmentName}</td>
                                <td>${salaryFormat}</td>
                                <td>${workStatus}</td>
                            </tr>`;
        //append trHTML vào tbody
        $('tbody').append(trHTML);
        //đẩy lên đầu
        // $('tbody').prepend(trHTML);
                            

        });
        // 3. Hiển thị dl lên table
    }).fail(function(res){
    })
    } catch (error) {
        console.log(error);
    }
  
}

function deleteEmployee(employeeId) {
    $.ajax({
        type: "DELETE",
        url: "http://cukcuk.manhnv.net/v1/Employees/" + employeeId,
        // async: false,
        success: function(result) {
            if (result != null) {
                fe = result;
                console.log("FOunded Emp", fe);
                location.reload();
            }
        },
        error: function(e) {
            console.log("ERROR: ", e);
        }
    });
}