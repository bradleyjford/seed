(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserListController', ['$scope', 'users', function ($scope, users) {
        $scope.users = users;
    }]);
})(angular);