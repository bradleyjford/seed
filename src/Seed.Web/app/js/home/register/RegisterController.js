(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('RegisterController', ['$scope',
        function ($scope) {
            $scope.model = {
                username: '',
                password: '',
                error: ''
            };

            $scope.register = function () {

            };
        }
    ]);
})(angular);