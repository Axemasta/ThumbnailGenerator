# Thumbnail Generator

Proof of concept to generate video thumbnails from files in realtime.

## Principle

iOS & Android have native api's for generating thumbnails so we are going to use them.

The most convenient way of accessing these api's was through a file path, so the video you use must be stored locally within the app filesystem.

### iOS

Using [AVAssetImageGenerator](https://developer.apple.com/documentation/avfoundation/avassetimagegenerator) we can produce a `UIImage` which can then be converted into a `byte[]` and used as an `ImageSource` at the maui level.
 
### Android

Using [ThumbnailUtils](https://developer.android.com/reference/android/media/ThumbnailUtils) we can produce a bitmap which can then be converted into a `byte[]` and used as an `ImageSource` at the maui level.

## Sample App

The sample app uses the `MediaPicker` to load a video already captured on the device. If you provide a video, the video will begin playing using the `MediaElement` and you have the option to generate a thumbnail. Currently the app generates a large thumbnail and displays it below.

| Android                                               | iOS                                           |
|-------------------------------------------------------|-----------------------------------------------|
| ![Android Screenshot](/assets/android-screenshot.png) | ![iOS Screenshot](/assets/ios-screenshot.png) |