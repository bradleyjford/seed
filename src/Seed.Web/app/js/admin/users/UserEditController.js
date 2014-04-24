(function (angular) {
    'use strict';

    var UserEditController = function ($scope, $stateParams) {
        $scope.userId = $stateParams.userId;
    };

    UserEditController.$inject = ['$scope', '$stateParams'];

    var module = angular.module('seedApp');

    module.controller('UserEditController', UserEditController);
})(angular);