<template>
    <p><span>Message:</span> {{ message }}</p>
</template>

<script>
module.exports = {
    data: function() {
        return { message: "Hello!" };
    }
}
</script>

<style lang="scss" scoped>
    body {
        p {
            font-size: 2em;
            display: flex;
            flex: 1 1 auto;
        }
    }
</style>