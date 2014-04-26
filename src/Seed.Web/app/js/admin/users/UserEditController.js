(function (angular) {
    'use strict';

    var UserEditController = function ($scope, $state, model) {
        $scope.model = model;

        $scope.save = function () {
            if ($scope.editUserForm.$valid)
            {
                $scope.model.$save(function () {
                    $state.go('^', null, { reload: true });
                });
            }
        };

        $scope.cancel = function() {
            $state.go('^');
        };
    };

    UserEditController.$inject = ['$scope', '$state', 'model'];

    var module = angular.module('seedApp.admin');

    module.controller('UserEditController', UserEditController);
})(angular);