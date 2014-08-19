(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserListController', ['$scope', 'users', 'UsersApi', function ($scope, users, UsersApi) {
        $scope.pagedItems = users;

        $scope.sortOrder = {
            property: 'Id',
            ascending: true
        };

        $scope.search = {
            query: ''
        };

        function getSortOrderString (sortOrder) {
            var direction = sortOrder.ascending ? 'asc' : 'desc';

            return sortOrder.property + ' ' + direction;
        }

        $scope.reloadData = function (page, pageSize, sortOrder, filter) {
            var requestData = {
                pageNumber: page,
                pageSize: pageSize,
                sortOrder: getSortOrderString(sortOrder),
                filterText: filter
            };

            return UsersApi.query(requestData, function (data) {
                $scope.pagedItems = data;
            });
        };

        $scope.pageChanged = function() {
            return $scope.reloadData($scope.pagedItems.pageNumber, $scope.pagedItems.pageSize, $scope.sortOrder, $scope.search.query);
        };

        $scope.orderBy = function (property) {
            if ($scope.sortOrder.property === property) {
                $scope.sortOrder.ascending = !$scope.sortOrder.ascending;
            }
            else {
                $scope.sortOrder.property = property;
                $scope.sortOrder.ascending = true;
            }

            return $scope.reloadData(1, $scope.pagedItems.pageSize, $scope.sortOrder, $scope.search.query);
        };

        $scope.search = function () {
            return $scope.reloadData(1, $scope.pagedItems.pageSize, $scope.sortOrder, $scope.search.query);
        };
    }]);
})(angular);
