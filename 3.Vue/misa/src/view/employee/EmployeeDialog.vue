<template>
    <div id="dialogEmployee" class="form-employee"
    :class="{'form-employee': isHide}"
    >
  <div class="model"></div>
  <div class="title-employee">
        <div class="left">THÔNG TIN NHÂN VIÊN</div>
        <div class="right"><img id="Cancel" src="../../assets/icon/x.svg" @click="btnCancelOnClick"/></div>
  </div>
  <div class="content-employee">
        <div class="left">
            <div class="avatar"></div>
            <div class="text">(Vui lòng chọn ảnh có định dạng .jpg, .jeg, .png, .gif.)</div>
        </div>
        <div class="right-box">
            <div class="top">
                <div class="title1">A.THÔNG TIN CHUNG:</div>
                <div class="line"></div>
                <form>
                    <div class="column1">
                        <label >Mã nhân viên (<span>*</span>)</label>
                        <br>
                        <input id="txtEmployeeCode" required type="text" name="mã" placeholder="Nhập mã..."
                        v-model="employee.EmployeeCode"
                        >
                        <br>
                        <label>Ngày sinh</label>
                        <br>
                        <input id="dateDateOfBirth" type="date" name="date"  v-model="employee.DateOfBirth">
                        <br>
                        <label>Số CMTND/Căn cước (<span>*</span>)</label>
                        <br>
                        <input type="text" required name="cmtnd" placeholder="Nhập CMTND..."  v-model="employee.IdentityNumber">
                        <br>
                        <label>Nơi cấp</label>
                        <br>
                        <input  type="text" name="place" placeholder="Nhập nơi cấp..." v-model="employee.IdentityPlace">
                        <br>
                        <label>Email (<span>*</span>)</label>
                        <br>
                        <input id="txtEmail" required type="text" name="email" placeholder="Nhập email..." v-model="employee.Email">
                    </div>
                    <div class="column2">
                        <label >Họ và tên (<span>*</span>)</label>
                        <br>
                        <input id="txtFullName" type="text" required name="tên" placeholder="Nhập họ và tên..." v-model="employee.FullName">
                        <br>
                        <label>Giới tính</label>
                        <br>
                        <div class="select-box box11">
                            <div class="options-container box11">
                              <div class="option box11">
                                <input type="radio" class="radio" name="category"/>
                                <label for="">Nam</label>
                              </div>
                    
                              <div class="option box11">
                                <input type="radio" class="radio" name="category" />
                                <label for="">Nữ</label>
                              </div>
                    
                              <div class="option box11">
                                <input type="radio" class="radio" name="category" />
                                <label for="">Khác</label>
                              </div>
                            </div>
                    
                            <div class="selected box11" >
                                Nam
                            </div>
                        </div>
                        <br>
                        <label>Ngày cấp</label>
                        <br>
                        <input type="date" name="ngayCap" v-model="employee.IdentityDate">
                        <br><br><br><br><br>
                        <p style="margin-top: 5px;margin-bottom: 0px;">Số điện thoại (<span>*</span>)</p>
                        <input id="txtPhoneNumber" type="text" required name="place" placeholder="Nhập SĐT..."  v-model="employee.PhoneNumber">
                    </div>
                </form>
            </div>
            <div class="bottom">
                <div class="title1">B.THÔNG TIN CÔNG VIỆC:</div>
                <div class="line"></div>
                <form>
                    <div class="column3">
                        <label>Vị trí</label>
                        <br>
                        <div class="select-box box22">
                            <div class="options-container box22">
                              <div class="option box22">
                                <input type="radio" value="" class="radio" name="category"/>
                                <label for="">Giám đốc</label>
                              </div>
                    
                              <div class="option box22">
                                <input type="radio" value="" class="radio" name="category" />
                                <label for="">Nhân viên Marketting</label>
                              </div>
                    
                              <div class="option box22">
                                <input type="radio" value="" class="radio" name="category" />
                                <label for="">Thu ngân</label>
                              </div>
                            </div>
                    
                            <div class="selected box22">
                                Giám đốc
                            </div>
                          </div>
                        <br>
                        <label>Mã số thuế cá nhân</label>
                        <br>
                        <input type="text" name="thuế" placeholder="Nhập mã số thuế..." v-model="employee.PersonalTaxCode">
                        <label>Ngày gia nhập công ty</label>
                        <br>
                        <input type="date" name="datecty" v-model="employee.JoinDate">
                    </div>
                    <div class="column4">
                        <label>Phòng ban</label>
                        <br>
                        <div class="select-box box33">
                            <div class="options-container box33">
                              <div class="option box33">
                                <input type="radio" value="" class="radio" name="category"/>
                                <label for="">Phòng nhân sự</label>
                              </div>
                    
                              <div class="option box33">
                                <input type="radio" value="" class="radio" name="category" />
                                <label for="">Phòng công nghệ</label>
                              </div>
                    
                              <div class="option box33">
                                <input type="radio" value="" class="radio" name="category" />
                                <label for="">Phòng Marketting</label>
                              </div>
                            </div>
                    
                            <div class="selected box33">
                                Phòng nhân sự
                            </div>
                          </div>
                        <br>
                        <label>Mức lương cơ bản</label>
                        <br>
                        <input class="money" id="txtSalary" type="text" name="luong" style="padding-right: 54px;" v-model="employee.Salary">
                        <span class="dvt"><i>(VNĐ)</i></span>
                        <br>
                        <label>Tình trạng công việc</label>
                        <br>
                        <div class="select-box box44">
                            <div class="options-container box44">
                              <div class="option box44">
                                <input type="radio" value="" class="radio" name="category"/>
                                <label for="">Đang làm việc</label>
                              </div>
                    
                              <div class="option box44">
                                <input type="radio" value="" class="radio" name="category" />
                                <label for="">Đang thử việc</label>
                              </div>
                    
                              <div class="option box44">
                                <input type="radio" value="" class="radio" name="category" />
                                <label for="">Đã nghỉ việc</label>
                              </div>
                            </div>
                    
                            <div class="selected box44">
                                Đang làm việc
                            </div>
                          </div>
                    </div>
                </form>
            </div>
          </div>
  </div>
  <div class="footer-employee">
      <div class="sub">
            <div class="button btn1"  @click="btnCancelOnClick"><p>Hủy</p></div>
            <div class="button btn2" @click="btnSaveOnClick"><img src="../../assets/icon/save-solid.svg" width="20px" height="20px">Lưu</div>
      </div>
  </div>
</div>
</template>
<script>
// export default {
//     name="EmployeeDialog"
// }
</script>
<style scoped>
/* @import '../../css/main.css'; */
@import '../../css/form/form-employee.css';
</style>

<script>
import axios from "axios";
export default({
    setup(){

    },
    props:{
        isHide:{
            type: Boolean,
            default: true,
            required: true
        },
        employeeId:{
            type: String,
            // default:null,
            required: true,
        },
        mode:{
          type: Number,
          required: true,
          default: 0, //0 là thêm, 1 là sửa
        }
    },
    methods:{

      // Đóng form
      // Author: DT.Trinh (29/07/2021)
      btnCancelOnClick(){
      let dlg = document.getElementById("dialogEmployee");
      dlg.style.display = "none";
      },

      // Thêm mới và sửa nhân viên
      // Author: DT.Trinh (29/07/2021)
      btnSaveOnClick(){
          let nv = this;
          if (this.mode == 0) {
            axios
            .post(`http://cukcuk.manhnv.net/v1/Employees`,nv.employee)
            .then(res => {
            console.log(res);
            alert("Thêm mới thành công")
            })
            .catch(e => {
              console.log(e);
            })
          } else {
              axios
            .put(`http://cukcuk.manhnv.net/v1/Employees/${nv.employeeId}`,nv.employee)
            .then(res => {
            console.log(res);
            alert("Sửa thành công")
            })
            .catch(e => {
              console.log(e);
            })
          }
      }

    },
    data(){
        return{
            employee: {},
        };
    },
    watch:{
        employeeId: function(){
          console.log(this.employeeId);
            let nv= this;
            // nv.employees = response.data;
            console.log(nv);
            axios
            .get(`http://cukcuk.manhnv.net/v1/Employees/${nv.employeeId}`)
            .then(res => {
            console.log(res.data);
            nv.employee = res.data;
            })
            .catch(e => {
              console.log(e);
            })
        },
        mode: function(){
          if (this.mode==0) {
            this.employee= {};
          }
        }
    },
    computed:{
        testEmployeeId: function(){
            return this.employeeId;
        }
    }
})
</script>