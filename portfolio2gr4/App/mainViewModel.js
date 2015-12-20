define(['knockout', 'jquery'], function (ko, $) {
	function viewModel() {
//OBJECTS:
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

		var getSuggestions = function (target, event) {
			switch (currentComponent()) {
				case "Users":
					//not implemented yet
					break;
				case "Annotations":
					//not implemented yet
					break;
				case "Questions":
					$.getJSON("api/questions/search_title/" + searchText(), function (result) {
						if (result.length >= 1) {
							var titles = $.map(result, function (q) {
								return { Title: q.Title, Url: q.Url };
							});
							suggestions(titles);
						}
					});
					break;
				case "History":
					//not implemented yet
					break;
			}
		};
		var searchFor = function (target, event) { 
			switch (currentComponent()) {
				case "Users":
					//not implemented yet
					break;
				case "Annotations":
					//not implemented yet
					break;
				case "Questions":
					console.log(searchText());
					$.getJSON("api/questions/search/" + searchText(), function (result) {
						if (result.length >= 1) {
							//var titles = $.map(result, function (q) {
							//    return q.Title;
							//});
							searchResult(result);
							viewData(searchResult);
							changeContent("Questions");
						} else {
							alert("no result found!");
						}
					});
					break;
				case "History":
					//not implemented yet
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