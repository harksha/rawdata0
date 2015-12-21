define(['knockout', 'jquery'], function (ko, $) {
	function viewModel() {
	    //OBJECTS:
	    var page = ko.observable(1);
	    var size = ko.ko.observable(10);
		var menuItems = ["Questions", "Users", "Annotations", "History"];
		var currentMenu = ko.observable("");

		var currentComponent = ko.observable(menuItems[0]);
		var changeContent = function (component) {
				currentComponent(component); 
				},
				viewData = ko.observable([]);
		//SEARCH:
		var searchText = ko.observable("");
		var suggestions = ko.observableArray([]);// for searchbar
		var searchResult = ko.observableArray([]);// for page body


//UTIL FUNCTIONS:
		isActive = function (menu) { 
			return menu === currentComponent();
		};
        // this function provide suggestions for search bar
		var getSuggestions = function (target, event) {
		    switch (currentMenu()) {
		        case "Users":
		            alert(currentMenu());
		            $.getJSON("/api/users/GetByKey/" + searchText(), function (result) {
		                if (result.length >= 1) {
		                    var user_names = $.map(result, function (n) {
		                        return { Name: n.Name, Url: n.Url };
		                    });
		                    suggestions(user_names);
		                }
		            });
		            break;
		        case "Annotations":
		            if (uid.length > 0) {
		                $.getJSON("/api/annotations/search/" + searchText() + "-" + uid, function (result) {
		                    if (result.length >= 1) {
		                        var annotation_bodies = $.map(result, function (n) {
		                            return { Body: n.Body, Url: n.Url };
		                        });
		                        searchResult(annotation_bodies);
		                    }
		                });
		            }
		            break;
		        case "Questions":
		            $.getJSON("api/questions/search_title/" + searchText(), function (result) {
		                if (result.length >= 1) {
		                    var question_titles = $.map(result, function (q) {
		                        return { Title: q.Title, Url: q.Url };
		                    });
		                    suggestions(question_titles);
		                }
		            });
		            break;
		        case "History":
		            if (uid.length > 0) {
		                $.getJSON("api/historys/search/" + searchText() + "-" + uid + "-" + size() + "-" + page(), function (result) {
		                    if (result.length >= 1) {
		                        var titles = $.map(result, function (q) {
		                            return { Body: q.Body, Url: q.Url };
		                        });
		                        suggestions(titles);
		                    }
		                });

		            }
		            break;
		    }
		    selecte_suggestion(currentMenu());// display the correct suggestion list based on current menu
		};
        // this function provide search result for search button 
		var searchFor = function (target, event) {
		    switch (currentMenu()) {
		        case "Users":
		            $.getJSON("/api/users/GetByKey/" + searchText()+"-"+size()+"-"+page(), function (result) {
		                if (result.length >= 1) {
		                    searchResult(result);
		                    currentView("users_search_view");
		                } else {
		                    alert("no result found!");
		                }
		            });
		            break;
		        case "Annotations":
		            if (uid.length === 0) {
		                var u = prompt("Fake login , Please enter your user id !", "9903");
		                uid = u;
		            }
		            $.getJSON("/api/annotations/search/" + searchText() + "-" + uid, function (result) {
		                if (result.length >= 1) {
		                    searchResult(result);
		                    currentView("annotations_search_view");
		                } else {
		                    alert("no result found!");
		                }
		            });
		            break;
		        case "Questions":
		            $.getJSON("api/questions/search_title/" + searchText(), function (result) {
		                if (result.length >= 1) {
		                    searchResult(result);
		                    currentView("questions_search_view");
		                } else {
		                    alert("no result found!");
		                }
		            });
		            break;
		        case "History":
		            if (uid.length === 0) {
		                var u = prompt("Fake login , Please enter your user id !", "9903");
		                uid = u;
		            }
		            $.getJSON("api/historys/search/" + searchText() + "-" + uid, function (result) {
		                if (result.length >= 1) {
		                    searchResult(result);
		                    currentView("history_search_view");
		                } else {
		                    alert("no result found!");
		                }
		            });
		            break;
		    }
		};
		var selecte_suggestion = function (currentMenu) {

		    switch (currentMenu) {
		        case "Users":
		            $("#user_suggestions").toggle(true);
		            $("#search_textbox").attr("list", "user_suggestionsList");
		            $("#question_suggestions").toggle(false);
		            $("#Annotation_suggestions").toggle(false);
		            $("#history_suggestions").toggle(false);
		            break;
		        case "Questions":
		            $("#user_suggestions").toggle(false);
		            $("#question_suggestions").toggle(true);
		            $("#search_textbox").attr("list", "question_suggestionList");
		            $("#Annotation_suggestions").toggle(false);
		            $("#history_suggestions").toggle(false);
		            break;
		        case "Annotations":
		            $("#user_suggestions").toggle(false);
		            $("#question_suggestions").toggle(false);
		            $("#Annotation_suggestions").toggle(true);
		            $("#search_textbox").attr("list", "Annotation_suggestionsList");
		            $("#history_suggestions").toggle(false);
		            break;
		        case "History":
		            $("#user_suggestions").toggle(false);
		            $("#question_suggestions").toggle(false);
		            $("#Annotation_suggestions").toggle(false);
		            $("#history_suggestions").toggle(true);
		            $("#search_textbox").attr("list", "history_suggestionsList");
		            break;
		    }
		};

		return {
			currentComponent: currentComponent,
			changeContent: changeContent,
			viewData: viewData,
			currentMenu: currentMenu,
			menuItems: menuItems,
			isActive: isActive,
			searchText: searchText,
			suggestions: suggestions,
			getSuggestions: getSuggestions,
			searchFor: searchFor,
			searchResult: searchResult
			 
		}
	};

	return viewModel;
});