(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saAuthorize', [function () {
        return function (scope, element, attrs) {
            var originalDisplay = element.css('display');

            element.css('display', 'none');

            scope.$watchCollection('user.roles', function () {
                if (attrs.saAuthorize === '' ||
                    scope.user.isInRole(attrs.saAuthorize)) {

                    element.css('display', originalDisplay);
                }
                else {
                    element.css('display', 'none');
                }
            });
        };
    }]);
})(angular);