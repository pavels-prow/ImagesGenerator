<template>
    <div>
        <div v-for="item in items" class="d-inline-block m-2">
            <div class="card" style="width: 18rem; min-height: 14rem; cursor: pointer;" :key="item.value" @click="selectItem(item)" :class="{'table-active': selectedItem === item.value}">
                <div class="card-body">
                    <h5 class="card-title">{{ item.name }}</h5>
                    <p class="card-text">{{ item.description }}</p>
                </div>
            </div>
        </div>
    </div>
</template>


<script>import axios from 'axios';
    // TODO: align cards content on center. Set max width: 100% from parent conponent
    export default {
        name: 'MySelect',
        props: ['apiUrl'],
        data() {
            return {
                items: [],
                selectedItem: null
            };
        },
        methods: {
            async fetchItems() {
                try {
                    const postData = {
                        // Укажите здесь необходимые данные для POST-запроса
                    };
                    const response = await axios.post(this.apiUrl, postData);
                    this.items = response.data;
                } catch (error) {
                    console.error('Ошибка при отправке POST-запроса:', error);
                }
            },
            selectItem(item) {
                this.selectedItem = item.value;
                this.emitSelection(item);
            },
            emitSelection(item) {
                this.$emit('selected', item.value);
            }
        },
        mounted() {
            this.fetchItems();
        }
    };</script>

<style>
    .table-active {
        background-color: #f8f9fa; /* Светло-серый цвет для выбранной строки */
    }
</style>
