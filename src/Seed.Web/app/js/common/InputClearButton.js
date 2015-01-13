(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saClearButton', ['$compile', '$timeout', function ($compile, $timeout) {
        return {
            restrict: 'A',
            require: 'ngModel',
            scope: {
                'model': '=ngModel'
            },

            link: function(scope, el, attrs, ctrl) {

                scope.enabled = el.val() !== '';

                scope.clear = function() {
                    el.val('');

                    $timeout(function() {
                       el[0].focus();
                    }, 0, false);
                };

                el.parent().addClass('has-feedback');

                var template = $compile('<span ng-show="enabled" class="fa fa-cirle-times form-control-feedback form-control-clear" aria-hidden="true"></span>')(scope);

                el.after(template);

                scope.$watch('model', function(newValue, oldValue) {
                    scope.enabled = el.val() !== '';
                });
            }
        };
    }]);

})(angular);