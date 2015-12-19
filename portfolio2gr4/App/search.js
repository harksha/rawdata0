 
var searchViewModel = (function () {
    var searchText = ko.observable("");
    var suggestions = ko.observableArray([]);// for searchbar
    var searchResult=ko.observableArray([]);// for page body
    //var visible = ko.observable(false);
    //var selected = false;
    //var toggle = function (target, event) {
    //    alert(selected);
    //    selected=!selected;
    //};


    var getSuggestions = function (target, event) {
        switch (navigationViewModel.currentMenu()) {
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
        alert("hello");
        switch (navigationViewModel.currentMenu()) {
            case "Users":
                //not implemented yet
                break;
            case "Annotations":
                //not implemented yet
                break;
            case "Questions":
                $.getJSON("api/questions/search/" + searchText(), function (result) {
                    if (result.length >= 1) {
                        //var titles = $.map(result, function (q) {
                        //    return q.Title;
                        //});
                        searchResult(result);
                        navigationViewModel.currentView("questions_search_view");
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
        searchText: searchText,
        suggestions: suggestions,
        getSuggestions: getSuggestions,
        searchFor:searchFor,
        searchResult: searchResult
        //visible: visible
        //selected: selected
        //toggle: toggle
    };
})();
//var QuestionViewModel = function () {
//    var title = ko.observable("");
//    var question_owner = ko.observable("");
//    var question_Url = ko.observable("");
//    var question_body = ko.observable("");
//    var question_creationDate = ko.observable("");
//    var comments = ko.observableArray([]);
//    var answers = ko.observableArray([]);
//    var getQuestionByUrl = function (url){
//        $.getJSON(url + searchText(), function (result) {
//            title = result.Title;
//            question_owner = ;
//        });
//    };
//    var getOwnerByOwnerId = function(ownerUserId){

//    };
//}();


//ko.applyBindings(searchViewModel);

//var substringMatcher = function (strs) {
//    return function findMatches(q, cb) {
//        var matches, substringRegex;

//        // an array that will be populated with substring matches
//        matches = [];

//        // regex used to determine if a string contains the substring `q`
//        substrRegex = new RegExp(q, 'i');

//        // iterate through the pool of strings and for any string that
//        // contains the substring `q`, add it to the `matches` array
//        $.each(strs, function (i, str) {
//            if (substrRegex.test(str)) {
//                matches.push(str);
//            }
//        });

//        cb(matches);
//    };
//};

////var states = viewModel.suggestions;

//$('#the-basics .typeahead').typeahead({
//    hint: true,
//    highlight: true,
//    minLength: 1
//},
//{
//    name: 'states',
//    source: substringMatcher(states)
//});
//$('#scrollable-dropdown-menu .typeahead').typeahead(null, {
//    name: 'countries',
//    limit: 10,
//    source: viewModel.suggestions
//});
////$('#scrollable-dropdown-menu .typeahead').typeahead(null, {
////    name: 'titles',
////    limit: 10,
////    source: stof.searchViewModel.suggestions
////});

////$(document).ready(function () {
////    var substringMatcher = function (strs) {
////        return function findMatches(q, cb) {
////            var matches, substringRegex;
////            alert("hello");
////            // an array that will be populated with substring matches
////            matches = [];

////            // regex used to determine if a string contains the substring `q`
////            substrRegex = new RegExp(q, 'i');

////            // iterate through the pool of strings and for any string that
////            // contains the substring `q`, add it to the `matches` array
////            $.each(strs, function (i, str) {
////                if (substrRegex.test(str)) {
////                    matches.push(str);
////                }
////            });

////            cb(matches);
////        };
////    };
////});





