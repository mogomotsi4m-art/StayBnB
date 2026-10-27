document.addEventListener('change', function (event) {
  if (event.target && event.target.id === 'Images') {
    const files = Array.from(event.target.files || []);
    const invalid = files.some(f => !['image/jpeg', 'image/png', 'image/webp'].includes(f.type) || f.size > 5 * 1024 * 1024);
    if (invalid) {
      alert('Please select JPG, PNG or WEBP images no larger than 5 MB each.');
      event.target.value = '';
    }
  }
});
