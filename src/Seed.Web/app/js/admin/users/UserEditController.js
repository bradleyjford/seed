(function (angular) {
    'use strict';

    var UserEditController = function ($scope, user) {
        $scope.model = user;
    };

    UserEditController.$inject = ['$scope', 'user'];

    var module = angular.module('seedApp.admin');

    module.controller('UserEditController', UserEditController);
})(angular);