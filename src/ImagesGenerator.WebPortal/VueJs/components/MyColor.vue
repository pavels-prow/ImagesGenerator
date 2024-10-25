<template>
    <div class="d-flex align-items-center justify-content-start">
        <div v-for="(color, index) in colors" :key="index" class="mx-2">
            <div :style="{ backgroundColor: color, width: '30px', height: '30px', borderRadius: '50%', cursor: 'pointer', border: selectedColor === color ? '4px solid black' : 'none' }"
                 @click="selectColor(color)"></div>
        </div>
    </div>
</template>

<script>export default {
  name: "MyColor",
  data() {
    return {
      colors: [
        "#FF5733", // Ярко-красный
        "#33FF57", // Ярко-зеленый
        "#3357FF", // Ярко-синий
        "#F833FF", // Ярко-розовый
        "#FFC300", // Ярко-желтый
        "#A3A3A3", // Светло-серый
        "#505050", // Темно-серый
        "#FF6E33"  // Оранжевый
      ],
      selectedColor: null
    };
  },
  methods: {
    selectColor(color) {
      this.selectedColor = color;
      this.$emit("selected", color);
    }
  }
};</script>

<style>
    /* Дополнительные стили можно добавить здесь, если необходимо */
</style>
