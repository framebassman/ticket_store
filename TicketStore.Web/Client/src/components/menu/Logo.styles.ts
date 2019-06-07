export const styles = {
  logo: {
    display: 'flex'
  },
  title: {
    width: 200
  },
  image: {
    maxWidth: '100%',
    marginTop: 8
  },
  description: {
    position: 'absolute' as 'absolute',
    left: 300,
    top: 27,
    fontSize: '10pt'
  },
  '@media (max-width: 600px)': {
    description: {
      display: 'none'
    }
  }
}
