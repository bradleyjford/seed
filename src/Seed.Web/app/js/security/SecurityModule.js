(function (angular) {
    'use strict';

    var securityModule = angular.module('seedApp.security', ['seedApp', 'seedApp.templates']);

    securityModule.config([
        '$routeProvider', '$httpProvider', '$locationProvider', '$provide',
        function ($routeProvider, $httpProvider, $locationProvider, $provide) {

        }]);
})(angular);