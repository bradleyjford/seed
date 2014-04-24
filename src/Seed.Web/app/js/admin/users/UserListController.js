(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    var UserListController = function ($scope, users) {
        $scope.users = users;
    };

    UserListController.$inject = ['$scope', 'users'];

    module.controller('UserListController', UserListController);
})(angular);