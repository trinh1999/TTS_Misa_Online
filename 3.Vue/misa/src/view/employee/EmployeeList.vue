<template>
    <div class="employee-list" >
      <div class="content-head">
          <div class="left">Danh sách nhân viên</div>
          <div id="btnAdd" class="button" @click="btnAddOnClick">
              <div class="icon"><img src="../../assets/icon/add.png" /></div>
              <div class="text">Thêm nhân viên</div>
          </div>
      </div>
      <div class="content-box">
          <div class="text-box-icon">
              <i class="input-icon"><img src="../../assets/icon/search.png"/></i>
              <input type="text" id="search-employee"  class="input-field" placeholder="Tìm kiếm theo mã, Tên hoặc Số điện thoại"/>
          </div>
          <div class="section s1">
            <div class="select-box box2">
              <div class="options-container box2">
                <div class="option box2">
                  <input type="radio" class="radio" name="category"/>
                  <label for="">Tất cả phòng ban</label>
                </div>
      
                <div class="option box2">
                  <input type="radio" class="radio" name="category" />
                  <label for="">Phòng nhân sự</label>
                </div>
      
                <div class="option box2">
                  <input type="radio" class="radio" name="category" />
                  <label for="science">Phòng đào tạo</label>
                </div>
              </div>
      
              <div class="selected box2">
                Tất cả phòng ban
              </div>
            </div>
          </div>
          <div class="section s2">
            <div class="select-box box3">
              <div class="options-container box3">
                <div class="option box3">
                  <input type="radio" class="radio" name="category"/>
                  <label for="">Tất cả vị trí</label>
                </div>
      
                <div class="option box3">
                  <input type="radio" class="radio" name="category" />
                  <label for="">Nhân viên Marketting</label>
                </div>
      
                <div class="option box3">
                  <input type="radio" class="radio" name="category" />
                  <label for="">Giám đốc</label>
                </div>
              </div>
      
              <div class="selected box3">
                Nhân viên Marketting
              </div>
            </div>
          </div>
          <div class="refresh"><img src="../../assets/icon/refresh.png"/></div>
      </div>
      <div class="content-table">
        <table class="table" id="listEmployees" width="100%">
              <thead>
                <tr>
                  <th style="width: 69px;">Mã nhân viên</th>
                  <th style="width: 105px;padding-right: 5px;">Họ và tên</th>
                  <th style="padding-left: 0; width: 115px;padding-right: 0;">Giới tính</th>
                  <th style="width: 60px; padding-left: 0;">Ngày sinh</th>
                  <th style="width: 75px;">Điện thoại</th>
                  <th style="width: 150px;">Email</th>
                  <th style="width: 117px;">Chức vụ</th>
                  <th style="width: 98px;padding-right: 0;">Phòng ban</th>
                  <th style="width: 96px; padding-left: 10px;">Mức lương cơ bản</th>
                  <th style="padding-left: 10px;">Tình trạng công việc</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="employee in employees" :key="employee.EmployeeId" @dblclick="EditEmployee(employee.EmployeeId)" @click.ctrl="DeleteEmployee(employee.EmployeeId)">
                  <td>{{employee.EmployeeCode}}</td>
                  <td>{{employee.FullName}}</td>
                  <td>{{employee.GenderName}}</td>
                  <td>{{employee.DateOfBirth}}</td>
                  <td>{{employee.PhoneNumber}}</td>
                  <td>{{employee.Email}}</td>
                  <td>{{employee.PositionName}}</td>
                  <td>{{employee.DepartmentName}}</td>
                  <td>{{employee.Salary}}</td>
                  <td>{{employee.WorkStatus}}</td>
                </tr>
              </tbody>
            </table>
      </div>
      <div class="footer">
          <div class="left">Hiển thị 1-10/1000 nhân viên</div>
          <div class="dot-next-prev">
              <img src="../../assets/icon/btn-firstpage.svg">
              <img src="../../assets/icon/btn-prev-page.svg">
              <span class="dot">1</span> 
              <span class="dot">2</span> 
              <span class="dot">3</span>
              <span class="dot">4</span>
              <img src="../../assets/icon/btn-next-page.svg">
              <img src="../../assets/icon/btn-lastpage.svg">
          </div>
          <div class="right">10 nhân viên/trang</div>
      </div>
      <EmployeeDialog 
      v-bind:isHide='isHide'
      v-bind:employeeId='employeeId'
      v-bind:mode='modeFormDetail'
      />
    </div>
</template>

<script>
import axios from "axios";
import EmployeeDialog from "./EmployeeDialog.vue";
export default {
    name:"EmployeePage",
    components: {EmployeeDialog},
     mounted() {
      var nv = this;
    //gọi API lấy dữ liệu
    axios.get(`http://cukcuk.manhnv.net/v1/Employees`)
    .then(response => {
      console.log(response);
      nv.employees = response.data;
    })
    .catch(e => {
      this.errors.push(e)
    })
    },
    methods: {
        // Hiển thị form chi tiết nv
        // Author: DT.Trinh (29/07/2021)
        btnAddOnClick(){
            let dlg = document.getElementById("dialogEmployee");
            dlg.style.display = "block";
            // let list = document.getElementById("List-employee");
            // list.style.opacity = "0.5";
            // list.style.position = "fixed";
            this.isHide =false;
            this.modeFormDetail= 0;
        },
        // Hiển thị form chi tiết nv để sửa
        // Author: DT.Trinh (29/07/2021)
        EditEmployee(empId){
            alert(empId);
            let dlg = document.getElementById("dialogEmployee");
            dlg.style.display = "block";
            this.employeeId = empId;
            this.modeFormDetail= 1;
            
        },
        // Xóa 1 nhân viên
        // Author: DT.Trinh (29/07/2021)
        DeleteEmployee(employee_id){
        if (confirm("Bạn muốn xóa nhân viên này?")) {
            this.employeeId = employee_id;
            let nv = this;
            axios
            .delete(`http://cukcuk.manhnv.net/v1/Employees/${nv.employeeId}`,nv.employee)
            .then(res => {
            console.log(res);
            })
            .catch(e => {
              console.log(e);
            })
          }
    }
         
    },
    data(){
        return {
            employees: [],
            employeeId: '',
            isHide: true,
            modeFormDetail:0
        };
    },
}
</script>

<style scoped>

</style>