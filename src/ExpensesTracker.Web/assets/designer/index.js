import Vue from "vue"
import Hello from "./components/hello.vue"

new Vue({
    el: '#designer',
    render: function(h) {
        return h(Hello, { props: { name: "Name" } });
    }
})