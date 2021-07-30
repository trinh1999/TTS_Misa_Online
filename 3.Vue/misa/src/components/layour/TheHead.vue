<template>
    <div class="header">
      <div class="header-left">
          <div class="toggle"></div>
          <div class="logo"></div>
      </div>
      <div class="header-right">
          <div class="dropdown">
            <div class="select-box box1">
              <div class="options-container box1">
                <div class="option box1">
                  <input type="radio" class="radio" name="category"/>
                  <label for="">Nhà hàng Biển Đông</label>
                </div>
      
                <div class="option box1">
                  <input type="radio" class="radio" name="category" />
                  <label for="">Nhà hàng Hải Quan</label>
                </div>
      
                <div class="option box1">
                  <input type="radio" class="radio" name="category" />
                  <label for="">Nhà hàng Bến Phà</label>
                </div>
              </div>
      
              <div class="selected box1">
                  Nhà hàng Biển Đông
              </div>
            </div>
          </div>
          <div class="avatar">
              <div class="text"><span>Nguyễn Văn Mạnh</span></div>
          </div>
      </div>
  </div>
</template>

<script>
export default {
    name: "TheHead",
};
</script>

<style scoped>
@import '../../css/layout/header.css';
</style>