<template>
  <div class="content">
    <EmployeeList/>
  </div>
</template>

<script>
// import axios from 'axios';
import EmployeeList from '../../view/employee/EmployeeList.vue';
export default {
    name: "Content",
    components: {EmployeeList},
  
};
</script>

<style scoped>
@import '../../css/layout/content.css';
</style>