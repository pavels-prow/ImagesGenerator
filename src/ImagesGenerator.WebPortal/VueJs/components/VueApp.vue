<template>
    <div>
        <div class="my-5">
            <h4>Выберете акцентный цвет.</h4>
            <MyColor v-on:selected="handleSelection1" />
        </div>
        <div class="my-5">
            <h4>Выберете предметную область из предложенных.</h4>
            <MySelect v-bind:api-url="'/api/subject-areas/list'"
                      v-on:selected="handleSelection2" />

        </div>
        <div class="my-5">
            <h4>Опишите ваш бизнес (до 500 символов):</h4>
            <textarea v-model="businessDescription" maxlength="500" rows="6"
                      class="full-width-textarea"></textarea>
        </div>
        <div class="my-5">
            <button class="btn btn-primary btn-lg" v-on:click="submitForm" v-bind:disabled="!selectedItem1 || !selectedItem2">
                Дальше
            </button>
        </div>
    </div>

</template>

<script>import MyColor from './MyColor.vue';
    import MySelect from './MySelect.vue';
    import axios from 'axios';

    export default {
        name: 'VueApp',
        components: { MyColor, MySelect },
        data() {
            return {
                selectedItem1: null,
                selectedItem2: null,
                businessDescription: ''
            };
        },
        methods: {
            handleSelection1(item) {
                this.selectedItem1 = item;
                console.log('Выбранный цвет:', item);
            },
            handleSelection2(item) {
                this.selectedItem2 = item;
                console.log('Выбранная предметная область:', item);
            },
            submitForm() {
                if (!this.selectedItem1 || !this.selectedItem2) {
                    alert('Пожалуйста, заполните все поля!');
                    return;
                }

                let data = {
                    selectedColor: this.selectedItem1,
                    selectedArea: this.selectedItem2,
                    businessDescription: this.businessDescription
                };

                axios.post('/api/images-generator/submit-order', data)
                    .then(response => {
                        if (response.data.success) {
                            window.location.href = response.data.redirectUrl;
                        }
                    })
                    .catch(error => {
                        console.error('Ошибка запроса:', error);
                        alert('Произошла ошибка при отправке данных. Попробуйте снова.');
                    });
            }
        }
    };</script>

<style>
.full-width-textarea {
    width: 100%;
    box-sizing: border-box;
}
</style>