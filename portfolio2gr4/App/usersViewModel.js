define(['knockout', 'jquery'], function (ko, $) {

    function viewModel(params) {
        var self = this;
        //var currentQuestion = ko.observable(QuesItem);
        var searchResult = ko.observable([]);
        this.title = "Users";
        if (typeof params === 'object') {
            $.getJSON("http://localhost:3133/api/users", function (result) {
                var rez = [];
                for (var i = 0; i < result.length; i++) {
                    var users = new UsersItem(result[i]);
                    rez.push(users);
                };
                searchResult(rez);
            });
        } else {
            var rez = [];
            for (var i = 0; i < params().length; i++) {
                var users = new UsersItem(params()[i]);
                rez.push(users);
            }
            searchResult(rez);
        }

        return {
            title: this.title,
            searchResult: searchResult
        }
    }
    function UsersItem(data) {
        var self = this;
        console.log(data);
        self.Url = data.Url;
        self.CreationDate = data.CreationDate;
        self.Location = data.Location;
        self.Name = data.Name;
    };
    return viewModel;
});