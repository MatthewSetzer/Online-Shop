var app = new Vue({
    el: '#app',
    data: {
        username: "",
        users: []
    },
    mounted() {
        this.getUsers();
    },
    methods: {
        createUser() {
            this.loading = true;
            axios.post('/users', { username: this.username })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                });
        },
        getUsers() {
            this.loading = true;
            axios.get('/users')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
    }
})