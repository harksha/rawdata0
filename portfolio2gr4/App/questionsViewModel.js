
define(['knockout', 'jquery'], function (ko, $) {

	function viewModel(params) {
		this.title = "Hello from questions";
		this.searchResult = params();
		console.log(params());
	}

	return viewModel;
});