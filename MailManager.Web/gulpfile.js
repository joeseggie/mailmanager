const gulp = require('gulp');
const concat = require('gulp-concat');

const vendorStyles = [
    "node_modules/bootstrap/dist/css/bootstrap.min.css"
];
const vendorFontAwesome = [
    "node_modules/font-awesome/css/font-awesome.min.css"
];
const vendorScripts = [
    "node_modules/jquery/dist/jquery.min.js",
    "node_modules/popper.js/dist/umd/popper.min.js",
    "node_modules/bootstrap/dist/js/bootstrap.min.js"
];

gulp.task('build-vendor-css', () => {
    return gulp.src(vendorStyles)
        .pipe(concat('vendor.min.css'))
        .pipe(gulp.dest('wwwroot/lib/vendor/css'));
});

gulp.task('build-fontawesome-css', () => {
    return gulp.src(vendorFontAwesome)
        .pipe(concat('fontawesome.min.css'))
        .pipe(gulp.dest('wwwroot/lib/fontawesome/css'));
});

gulp.task('build-vendor-js', () => {
    return gulp.src(vendorScripts)
        .pipe(concat('vendor.min.js'))
        .pipe(gulp.dest('wwwroot/lib/vendor/js'));
});