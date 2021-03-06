﻿require.config({
	baseUrl: "/",
	paths: {
		"jquery": "Scripts/jquery-2.1.4.min",
		"knockout": "Scripts/knockout-3.4.0",
		"knockout.validation": "Scripts/knockout.validation.min",
		"bootstrap": "Scripts/bootstrap.min"
	}
	,
	shim: { 
  	"bootstrap": {
  		deps: ["jquery"],
  		exports: "$.fn.popover"
  	},
  	"knockout.validation": {
  		"deps": ["knockout"]
  	},
	enforceDefine: true
	}
});
require(['jquery', 'bootstrap'], function ($) {
	return {};
});