class Formatter {
    public static extractFields(data: []) {
        return data.map(({ id, name }: { id: Number, name: String}) => ({ id, name }))
    }
}

export default Formatter