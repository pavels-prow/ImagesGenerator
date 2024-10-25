// VueJs/main.js
import { createApp } from 'vue';
import VueApp from './components/VueApp.vue';
import VueApp2 from './components/VueApp2.vue';

createApp(VueApp).mount('#vue-app');
createApp(VueApp2).mount('#vue-app-2');
