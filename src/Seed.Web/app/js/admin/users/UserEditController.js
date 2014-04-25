(function (angular) {
    'use strict';

    var UserEditController = function ($scope, $state, model) {
        $scope.model = model;

        $scope.cancel = function() {
            $state.go('^');
        };
    };

    UserEditController.$inject = ['$scope', '$state', 'model'];

    var module = angular.module('seedApp.admin');

    module.controller('UserEditController', UserEditController);
})(angular);