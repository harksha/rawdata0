require.config({
	baseUrl: "/",
	paths: {
		jquery: "Scripts/jquery-2.1.4.min",
		knockout: "Scripts/knockout-3.4.0",
		bootstrap: "Scripts/bootstrap.min"
	}
	,
	shim: { 
  	"bootstrap": {
  		deps: ["jquery"],
  		exports: "$.fn.popover"
  	},
	enforceDefine: true
	}
});
require(['jquery', 'bootstrap'], function ($) {
	return {};
});