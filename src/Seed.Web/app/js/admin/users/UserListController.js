(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserListController', ['$scope', 'users', 'UsersApi', function ($scope, users, UsersApi) {
        $scope.pagedItems = users;

        $scope.pageChanged = function() {
            var page = $scope.pagedItems.pageNumber;

            return UsersApi.query({ pageNumber: page, pageSize: $scope.pagedItems.pageSize }, function (data) {
                $scope.pagedItems = data;
            });
        };
    }]);
})(angular);