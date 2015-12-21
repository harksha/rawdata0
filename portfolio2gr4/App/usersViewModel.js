define(['knockout', 'jquery'], function (ko, $) {

    function viewModel(params) {
        var self = this;
        var currentUser = ko.observable(UsersItem);
        var showSingleUser = ko.observable(false);
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
        function getSingleUser(data) {

            currentUser(data);
            showSingleUser(true);
            console.log(currentUser());

        }

        function goBack() {
            showSingleUser(false);
        }
        return {
            title: this.title,
            searchResult: searchResult,
            currentUser: currentUser,
            showSingleUser: showSingleUser,
            getSingleUser: getSingleUser,
            goBack: goBack
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