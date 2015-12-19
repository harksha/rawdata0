
var masterVM = masterVM || (function () {
    this.navigationViewModel = navigationViewModel;
    this.searchViewModel = searchViewModel;
})();
var navigationViewModel = (function () {
    var Body = ko.observable("Default");
    var currentMenu = ko.observable("");
    var menuItems = ["Users", "Annotations", "Questions", "History"],
        currentView = ko.observable("users_view"),
        data = ko.observableArray([]),
        questions = ko.observableArray([]),
		users = ko.observableArray([]),
		history = ko.observableArray([]);  
    GoTo = function( ) {     
            $.getJSON("http://localhost:3133/api/questions/7664" , function (result) {
                questions([]);
                questions.push(new QuesItem(result));
                    
            });
        
        },
        showContent = function (menu) {
            var toggle = $("#navbar-toggle-button").attr("aria-expanded");
            if (toggle = (toggle === "true")) {
                $("#navbar-collapse").click(function () {
                    $("#navbar-toggle-button").trigger('click');
                });
            }
            var name = menu.toLowerCase();
            currentView(name + "_view");
            currentMenu(menu);
            $("#search_textbox").attr("placeholder", "Search " + currentMenu());
            if (name === "history") {
                name = "users/108/historys";
                //$("#suggestionList option").attr(" data-bind", "value:$data.Title");
                history([]);
                $.getJSON("http://localhost:3133/api/" + name, function (result) {
					
                    for(var i=0;i<result.length;i++){
                        history.push(new HistoryItem(result[i]));
                        //console.log("res:");
                        //console.log(history());
                    };
					 
                });
            } else if (name === "users") {
                name = "users";
                users([]);
                //$("#suggestionList option").attr(" data-bind", "value:$data.Title");
                $.getJSON("http://localhost:3133/api/" + name, function (result) {
					
                    for(var i=0;i<result.length;i++){
                        users.push(new UsersItem(result[i]));
                        //console.log("res:");
                        //console.log(history());
                        function uri() {

                        } 
                    };
					 
                });
            } else if (name === "questions") {
                name = "questions/10-1";
                questions([]);
               // $("#suggestionList option").attr(" data-bind", "value:$data.Title");
                $.getJSON("http://localhost:3133/api/" + name, function (result) {

                    for (var i = 0; i < result.length; i++) {
                        questions.push(new QuesItem(result[i]));
                        //console.log("res:");
                        //console.log(history());
                    };

                });
            } else {
                $.getJSON("http://localhost:3133/api/" + name, function (result) {
                    //console.log(result);
                    data(result);
                    console.log("res:");
                    console.log(data());
                });
            }

        },
       
        //add annotation button
        AddData = function () {
            $.ajax({
                type: "POST",
                url: "http://localhost:3133/api/annotations",
                data: ko.toJSON({Body: this.Body }),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    alert("added");
                },
                error: function (err) {
                    alert(err.status + " - " + err.statusText);
                }
            });
        };

    isActive = function (menu) {
        return menu === currentMenu();
    },
showList = ko.observable(true);

    function HistoryItem(data) {
        var self = this;
        console.log(data);
        self.Url = data.Url;
        self.userId = data.UserId;
        self.body = data.Body;
        self.searchDate = data.SearchDate;
		
    };
    function UsersItem(data) {
        var self = this;
        console.log(data);
        self.Url = data.Url;
        self.CreationDate = data.CreationDate;
        self.Location = data.Location;
        self.Name = data.Name;
        

    };

    function QuesItem(data) {
        var self = this;
        console.log(data);
        self.Url = data.Url;
        self.CreationDate = data.CreationDate;
        self.Body = data.Body;
        self.Title = data.Title;
        self.Owner = data.Owner;
	    

    };
    return {
        currentMenu:currentMenu,
        showContent: showContent,
        currentView: currentView,
        data: data,
        history: history,
        menuItems: menuItems,
        isActive: isActive,
        showList: showList,
        users: users,
        questions: questions,
        Body: Body,
        AddData: AddData,
        GoTo: GoTo
 
    };
}());

//navigationViewModel.showContent("users");
ko.applyBindings(masterVM);
$("#Users_menu").trigger('click');

   

