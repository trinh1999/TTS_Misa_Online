<template>
  <div id="app">
    <Header />
    <Menu />
    <Content/>
  </div>
</template>

<script>
import Header from './components/layour/TheHead.vue'
import Menu from './components/layour/TheMenu.vue'
import Content from './components/layour/TheContent.vue'

export default {
  name: 'App',
  components: {
    Header, Menu, Content,
  }
}
</script>

<style>
@import './css/main.css';
</style>
