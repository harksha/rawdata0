define(['knockout', 'jquery', 'knockout.validation'], function (ko, $, validation) {
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
		var searchText = ko.observable("").extend({
			required: true,
			minLength: 3
		});
		var suggestions = ko.observableArray([]);// for searchbar
		var searchResult = ko.observableArray([]);// for page body
		var showSuggestions = ko.observable(false);

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
					if (searchText.isValid()) { 
						$.getJSON("api/questions/search_title/" + searchText() + "-10-1", function (result) {
							console.log(result);
							if (result.length >= 1) {
								showSuggestions(true);
								var titles = $.map(result, function (q) {
									return { Title: q.Title, Url: q.Url };
								});
								suggestions(titles);
							}
						});
					}
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
							searchResult(result);
							viewData(searchResult);
							changeContent("Questions");
						} else {
							console.log("no result found!");
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
			showSuggestions: showSuggestions,
			searchFor: searchFor,
			searchResult: searchResult

		}
	};

	return viewModel;
});