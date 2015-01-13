module.exports = function(config) {
  config.set({
    basePath : '../',

    files : [
      'bower_components/angular/angular.js',
      'bower_components/jquery/dist/jquery.js',
      'bower_components/angular-ui-router/release/angular-ui-router.js',
      'bower_components/angular-resource/angular-resource.js',
      'bower_components/angular-local-storage/angular-local-storage.js',
      'bower_components/angular-mocks/angular-mocks.js',
      'app/js/app.js',
      'app/js/**/*Module.js',
      'app/js/**/*.js',
      'test/unit/**/*.js'
    ],

    exclude : [

    ],

    autoWatch : true,

    frameworks: ['jasmine'],

    browsers : ['Chrome'],

    plugins : [
      'karma-junit-reporter',
      'karma-chrome-launcher',
      'karma-jasmine'
    ],

    junitReporter : {
      outputFile: 'test_out/unit.xml',
      suite: 'unit'
    }
  });
};
