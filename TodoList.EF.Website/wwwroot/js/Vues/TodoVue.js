﻿var TodoVue = new Vue({
    el: "#TodoApp",
    data: {
        todos: [
            { 'id': 0, 'name': 'Loading Todos', 'completed': false}
        ]
    },
    methods: {
        toggleCompleted: function (id) {
            console.log("Completing " + id);
        },
        removeTodo: function (id) {
            console.log("Removing " + id);
        }
    },
    created: function () {
        this.$http.get('todo').then(function (res) {
            console.log(res.data);

            if (res.ok) {
                var todosToAdd = res.data.todos;

                for (var i = 0; i < todosToAdd.length; i++) {
                    var todo = todosToAdd[i];
                    this.todos.$set(i, {
                        _id: todo._id,
                        name: todo.name,
                        completed: todo.completed
                    });
                }
            }
            
        }, function (errorRes) {
            console.error(errorRes.json());
        });
    } 
})