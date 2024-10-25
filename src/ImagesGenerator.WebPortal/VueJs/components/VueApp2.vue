<template>
    <div>
        <div v-if="loading">
            Ваш запрос обрабатывается...
            <div class="spinner-grow text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
        <div v-else>           
            <div class="my-5">
                <h4>Перевод на английский язык:</h4>
                <p>{{ translation }}</p>
            </div>
            <div class="my-5">
                <h4>Мы извлекли следующие ключевые слова:</h4>
                <div>
                    <span v-for="keyword in keywords" :key="keyword" class="badge bg-secondary me-2">{{ keyword }}</span>
                </div>
            </div>
            <div class="my-5">
                <h4>Генерируем изображения:</h4>
                <div v-if="images.length === 0" class="image-gallery">
                    <div v-for="i in 4" :key="i" class="image-placeholder">
                        <div class="spinner-grow text-primary" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>
                </div>
                <div v-else class="image-gallery mt-4">
                    <div v-for="(image, index) in images" :key="index" class="image-generated">
                        <img :src="image" class="img-fluid" alt="Generated Image">
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'VueApp2',
        data() {
            return {
                loading: true,
                translation: '',
                keywords: [],
                images: [],
                api1resultReady: false, // Состояние готовности результатов api1
                api1url: '',
                api2url: ''
            };
        },
        mounted() {
            this.api1url = this.$el.parentElement.dataset.api1url;
            this.api2url = this.$el.parentElement.dataset.api2url;
            this.fetchApi1Data();
        },
        methods: {
            fetchApi1Data() {
                axios.post(this.api1url)
                    .then(response => {
                        this.translation = response.data.translation;
                        this.keywords = response.data.keywords;
                        this.loading = false;
                        this.api1resultReady = true;
                        this.fetchApi2Data();
                    })
                    .catch(error => {
                        console.error('There was an error!', error);
                        this.loading = false;
                    });
            },
            fetchApi2Data() {
                axios.post(this.api2url)
                    .then(response => {
                        console.log(response.data);
                        this.images = response.data.images;
                        this.loading = false;
                    })
                    .catch(error => {
                        console.error('There was an error with Api2!', error);
                        this.loading = false;
                    });
            }
        }
    };</script>

<style>
    .image-gallery {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        gap: 10px;
    }

    .image-placeholder {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 300px;
        height: 200px;
        border: 1px solid #ddd;
    }
    .image-generated {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 300px;
        height: 300px;
        border: 1px solid #ddd;
    }

    .spinner-grow {
        width: 2rem;
        height: 2rem;
        border-width: 3px;
    }

    /* Добавляем стили для бейджиков Bootstrap */
    .badge {
        font-size: 0.8em; /* Уменьшаем размер шрифта */
        padding: 0.25em 0.6em; /* Настраиваем отступы */
    }
</style>
