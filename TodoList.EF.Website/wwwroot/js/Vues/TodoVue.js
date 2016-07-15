var TodoVue = new Vue({
    el: "#TodoApp",
    data: {
        editMode: false,
        newTodoName: "",
        todos: [
           // { 'id': 0, 'name': 'Loading Todos', 'completed': false}
        ]
    },
    methods: {
        toggleEditMode: function () {
            console.log(this.editMode);
            this.$set('editMode', !this.editMode);
            if (this.editMode == false) {
                this.updateTodos();
            }
        },
        removeTodo: function (id) {
            console.log("Removing " + id);
            this.$http.delete('todo/' + id).then(function (res) {

                if (res.ok && res.data.success) {
                    populateListFromAPI();
                } else {
                    console.error("Error deleting todo");
                }
            }, function (errorRes) {
                    console.error(errorRes.data)
            });
        },
        createTodo: function () {
            this.$http.post('todo', { Name: this.newTodoName }).then(
                function (res) {
                    this.$set('newTodoName', "");
                    populateListFromAPI();
                }, function (errorRes) {
                    console.error("could not create todo");
            });
            this.$set('newTodoName', "");
        },
        updateTodos: function () {
            var completedTodos = 0;
            var targetTodos = this.todos.length;

            var onUpdateError = function (err) {
                console.error(err);
            }

            for (var i in this.todos) {
                var t = this.todos[i];
                console.log(t.id + " " + t.completed);
                this.$http.put('todo/' + t.id, {
                    Name: t.name,
                    Completed: t.completed
                }).then(function (res) {

                    if (res.ok && res.data.success) {
                        completedTodos += 1;
                        console.log("Completed todo " + t.id);
                        if (completedTodos >= targetTodos) {
                            console.log("Completed updates");
                            populateListFromAPI(t);
                        }
                    } else { onUpdateError(res.data); }
                    
                }, function () {
                    onUpdateError("Http Error");
                });
            }
        }
    },
    created: populateListFromAPI 
})

function populateListFromAPI () {
    Vue.http.get('todo').then(function (res) {
        console.log(res.data);

        if (res.ok) {
            var todosToAdd = res.data.todos;

            TodoVue.todos = [];

            for (var i = 0; i < todosToAdd.length; i++) {
                var todo = todosToAdd[i];
                TodoVue.todos.$set(i, {
                    id: todo.id,
                    name: todo.name,
                    completed: todo.completed
                });
            }
        }
            
    }, function (errorRes) {
        console.error(errorRes.data);
    });
}

$.ctrl('E', function () {
    TodoVue.toggleEditMode();
});