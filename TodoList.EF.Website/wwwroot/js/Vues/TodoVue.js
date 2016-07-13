var TodoVue = new Vue({
    el: "#TodoApp",
    data: {
        editMode: false,
        todos: [
           // { 'id': 0, 'name': 'Loading Todos', 'completed': false}
        ]
    },
    methods: {
        toggleEditMode: function () {
            console.log(this.editMode);
            this.$set('editMode', !this.editMode);
        },
        removeTodo: function (id) {
            console.log("Removing " + id);
            this.$http.delete('todo/' + id).then(function (res) {

                if (res.ok && res.data.success) {
                    onCreate();
                } else {
                    console.error("Error deleting todo");
                }
            }, function (errorRes) {
                    console.error(errorRes.data)
            });
        }
    },
    created: onCreate 
})

function onCreate () {
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